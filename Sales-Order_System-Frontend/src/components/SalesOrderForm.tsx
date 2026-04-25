'use client'

import { useState, useEffect, useCallback } from 'react'
import { useRouter } from 'next/navigation'
import { useAppDispatch, useAppSelector } from '../hooks/useRedux'
import { fetchCustomers, type Customer } from '../redux/slices/customersSlice'
import { fetchItems } from '../redux/slices/itemsSlice'
import { Order, OrderItem, addOrderLocally } from '../redux/slices/ordersSlice'
import { calculateExclAmount, calculateTaxAmount, calculateInclAmount, generateId, formatDate } from '../utils/calculations'
import { ordersService } from '../services/ordersService'

interface SalesOrderFormProps {
  existingOrder?: Order | null
}

export default function SalesOrderForm({ existingOrder }: SalesOrderFormProps) {
  const router = useRouter()
  const dispatch = useAppDispatch()
  const { customers } = useAppSelector((state) => state.customers)
  const { items } = useAppSelector((state) => state.items)

  const [selectedCustomer, setSelectedCustomer] = useState<Customer | null>(null)
  const [address1, setAddress1] = useState('')
  const [address2, setAddress2] = useState('')
  const [address3, setAddress3] = useState('')
  const [suburb, setSuburb] = useState('')
  const [state, setState] = useState('')
  const [postCode, setPostCode] = useState('')
  const [invoiceNo, setInvoiceNo] = useState('')
  const [invoiceDate, setInvoiceDate] = useState(formatDate(new Date()))
  const [referenceNo, setReferenceNo] = useState('')
  const [note, setNote] = useState('')
  const [orderItems, setOrderItems] = useState<OrderItem[]>([])
  const [isSaved, setIsSaved] = useState(false)
  const [orderId, setOrderId] = useState<number>()

  useEffect(() => {
    dispatch(fetchCustomers())
    dispatch(fetchItems())
  }, [dispatch])

  useEffect(() => {
    if (existingOrder) {
      const customer = customers.find(c => c.clientId === existingOrder.clientId)
      setSelectedCustomer(customer || null)
      setAddress1(customer?.address1 || '')
      setAddress2(customer?.address2 || '')
      setAddress3(customer?.address3 || '')
      setSuburb(customer?.suburb || '')
      setState(customer?.state || '')
      setPostCode(customer?.postCode || '')
      setInvoiceNo(existingOrder.invoiceNo)
      setInvoiceDate(existingOrder.invoiceDate)
      setReferenceNo(existingOrder.referenceNo)
      setNote(existingOrder.note)
      setOrderItems(existingOrder.items)
      setOrderId(existingOrder.orderId)
      setIsSaved(true)
    } else {
      setOrderId(generateId())
      setInvoiceNo(`INV-${Date.now().toString().slice(-6)}`)
    }
  }, [existingOrder, customers])

  const handleCustomerChange = (id: number) => {
    const customer = customers.find(c => c.clientId === id)
    setSelectedCustomer(customer || null)
    if (customer) {
      setAddress1(customer.address1)
      setAddress2(customer.address2)
      setAddress3(customer.address3)
      setSuburb(customer.suburb)
      setState(customer.state)
      setPostCode(customer.postCode)
    }
  }

  const addNewRow = useCallback(() => {
    const newItem: OrderItem = {
      orderId: generateId(),
      itemCode: '',
      description: '',
      note: '',
      quantity: 0,
      price: 0,
      taxRate: 0,
      exclAmount: 0,
      taxAmount: 0,
      inclAmount: 0,
    }
    setOrderItems(prev => [...prev, newItem])
  }, [])

  const updateOrderItem = (id: number, field: keyof OrderItem, value: string | number) => {
    setOrderItems(prev => prev.map(item => {
      if (item.orderId !== id) return item

      const updatedItem = { ...item, [field]: value }

      // If itemCode or description changed, update both and price
      if (field === 'itemCode') {
        const selectedItem = items.find(i => i.itemCode === value)
        if (selectedItem) {
          updatedItem.description = selectedItem.description
          updatedItem.price = selectedItem.price
        }
      } else if (field === 'description') {
        const selectedItem = items.find(i => i.description === value)
        if (selectedItem) {
          updatedItem.itemCode = selectedItem.itemCode
          updatedItem.price = selectedItem.price
        }
      }

      // Recalculate amounts
      const exclAmount = calculateExclAmount(updatedItem.quantity, updatedItem.price)
      const taxAmount = calculateTaxAmount(exclAmount, updatedItem.taxRate)
      const inclAmount = calculateInclAmount(exclAmount, taxAmount)

      return {
        ...updatedItem,
        exclAmount,
        taxAmount,
        inclAmount,
      }
    }))
  }

  const totalExcl = orderItems.reduce((sum, item) => sum + item.exclAmount, 0)
  const totalTax = orderItems.reduce((sum, item) => sum + item.taxAmount, 0)
  const totalIncl = orderItems.reduce((sum, item) => sum + item.inclAmount, 0)

  const handleSaveOrder = async () => {
    if (!selectedCustomer) {
      alert('Please select a customer')
      return
    }

    const order: Order = {
      orderId: orderId!,
      clientId: selectedCustomer.clientId,
      customerName: selectedCustomer.name,
      address1: selectedCustomer.address1,
      address2: selectedCustomer.address2,
      address3: selectedCustomer.address3,
      suburb: selectedCustomer.suburb,
      state: selectedCustomer.state,
      postCode: selectedCustomer.postCode,
      invoiceNo,
      invoiceDate,
      referenceNo,
      note,
      items: orderItems,
      totalExcl,
      totalTax,
      totalIncl,
    }

    try {
      if (existingOrder) {
        await ordersService.updateOrder(order)
      } else {
        await ordersService.saveOrder(order)
      }
      dispatch(addOrderLocally(order))
      setIsSaved(true)
      alert('Order saved successfully!')
    } catch (error) {
      console.error('Error saving order:', error)
      // Still mark as saved for demo purposes
      dispatch(addOrderLocally(order))
      setIsSaved(true)
      alert('Order saved locally!')
    }
  }

  const handlePrint = async () => {
    if (!isSaved) return

    try {
      const pdfBlob = await ordersService.printOrder(orderId!)
      const url = window.URL.createObjectURL(pdfBlob)
      window.open(url, '_blank')
    } catch (error) {
      console.error('Error printing order:', error)
      alert('Print functionality requires backend connection')
    }
  }

  // Ensure there's always at least one empty row
  useEffect(() => {
    if (orderItems.length === 0) {
      addNewRow()
    }
  }, [orderItems.length, addNewRow])

  return (
    <div className="space-y-4">
      {/* Action Buttons */}
      <div className="flex gap-2">
        <button
          onClick={handleSaveOrder}
          className="flex items-center gap-1 px-3 py-1 bg-gray-200 border border-gray-400 text-sm hover:bg-gray-300"
        >
          <span className="text-blue-600">✓</span> Save Order
        </button>
        <button
          onClick={handlePrint}
          disabled={!isSaved}
          className={`flex items-center gap-1 px-3 py-1 border border-gray-400 text-sm ${
            isSaved ? 'bg-gray-200 hover:bg-gray-300 cursor-pointer' : 'bg-gray-100 text-gray-400 cursor-not-allowed'
          }`}
        >
          Print
        </button>
        <button
          onClick={() => router.push('/')}
          className="flex items-center gap-1 px-3 py-1 bg-gray-200 border border-gray-400 text-sm hover:bg-gray-300"
        >
          Back to Home
        </button>
      </div>

      {/* Form Fields */}
      <div className="grid grid-cols-2 gap-8">
        {/* Left Column - Customer Details */}
        <div className="space-y-2">
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Customer Name</label>
            <select
              value={selectedCustomer?.clientId || ''}
              onChange={e => handleCustomerChange(e.target.value ? parseInt(e.target.value) : 0)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm bg-white"
            >
              <option value="">Select Customer</option>
              {customers.map((customer) => (
                <option key={customer.clientId} value={customer.clientId}>
                  {customer.name}
                </option>
              ))}
            </select>
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Address 1</label>
            <input
              type="text"
              value={address1}
              onChange={(e) => setAddress1(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Address 2</label>
            <input
              type="text"
              value={address2}
              onChange={(e) => setAddress2(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Address 3</label>
            <input
              type="text"
              value={address3}
              onChange={(e) => setAddress3(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Suburb</label>
            <input
              type="text"
              value={suburb}
              onChange={(e) => setSuburb(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">State</label>
            <input
              type="text"
              value={state}
              onChange={(e) => setState(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Post Code</label>
            <input
              type="text"
              value={postCode}
              onChange={(e) => setPostCode(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
        </div>

        {/* Right Column - Invoice Details */}
        <div className="space-y-2">
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Invoice No.</label>
            <input
              type="text"
              value={invoiceNo}
              onChange={(e) => setInvoiceNo(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Invoice Date</label>
            <input
              type="date"
              value={invoiceDate}
              onChange={(e) => setInvoiceDate(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Reference no</label>
            <input
              type="text"
              value={referenceNo}
              onChange={(e) => setReferenceNo(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
          <div className="flex items-center gap-2">
            <label className="w-24 text-sm text-gray-700">Note</label>
            <input
              type="text"
              value={note}
              onChange={(e) => setNote(e.target.value)}
              className="flex-1 border border-gray-400 px-2 py-1 text-sm"
            />
          </div>
        </div>
      </div>

      {/* Items Table */}
      <div className="border border-gray-400 overflow-auto">
        <table className="w-full border-collapse text-sm">
          <thead>
            <tr className="bg-gray-300">
              <th className="border border-gray-400 px-2 py-1 text-left w-24">Item Code</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-32">Description</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-20">Note</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-20">Quantity</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-20">Price</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-16">Tax</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-24">Excl Amount</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-24">Tax Amount</th>
              <th className="border border-gray-400 px-2 py-1 text-left w-24">Incl Amount</th>
            </tr>
          </thead>
          <tbody>
            {orderItems.map((item) => (
              <tr key={item.orderId} className="bg-white">
                <td className="border border-gray-400 p-0">
                  <select
                    value={item.itemCode}
                    onChange={(e) => updateOrderItem(item.orderId, 'itemCode', e.target.value)}
                    className="w-full px-1 py-1 text-sm border-0 bg-white"
                  >
                    <option value="">Select</option>
                    {items.map((i) => (
                      <option key={i.id} value={i.itemCode}>
                        {i.itemCode}
                      </option>
                    ))}
                  </select>
                </td>
                <td className="border border-gray-400 p-0">
                  <select
                    value={item.description}
                    onChange={(e) => updateOrderItem(item.orderId, 'description', e.target.value)}
                    className="w-full px-1 py-1 text-sm border-0 bg-white"
                  >
                    <option value="">Select</option>
                    {items.map((i) => (
                      <option key={i.id} value={i.description}>
                        {i.description}
                      </option>
                    ))}
                  </select>
                </td>
                <td className="border border-gray-400 p-0">
                  <input
                    type="text"
                    value={item.note}
                    onChange={(e) => updateOrderItem(item.orderId, 'note', e.target.value)}
                    className="w-full px-1 py-1 text-sm border-0"
                  />
                </td>
                <td className="border border-gray-400 p-0">
                  <input
                    type="number"
                    value={item.quantity || ''}
                    onChange={(e) => updateOrderItem(item.orderId, 'quantity', parseFloat(e.target.value) || 0)}
                    className="w-full px-1 py-1 text-sm border-0"
                  />
                </td>
                <td className="border border-gray-400 p-0">
                  <input
                    type="number"
                    value={item.price || ''}
                    readOnly
                    className="w-full px-1 py-1 text-sm border-0 bg-gray-100"
                  />
                </td>
                <td className="border border-gray-400 p-0">
                  <input
                    type="number"
                    value={item.taxRate || ''}
                    onChange={(e) => updateOrderItem(item.orderId, 'taxRate', parseFloat(e.target.value) || 0)}
                    className="w-full px-1 py-1 text-sm border-0"
                    placeholder="%"
                  />
                </td>
                <td className="border border-gray-400 px-1 py-1 bg-gray-50">
                  {item.exclAmount.toFixed(2)}
                </td>
                <td className="border border-gray-400 px-1 py-1 bg-gray-50">
                  {item.taxAmount.toFixed(2)}
                </td>
                <td className="border border-gray-400 px-1 py-1 bg-gray-50">
                  {item.inclAmount.toFixed(2)}
                </td>
              </tr>
            ))}
            {/* Empty rows for visual consistency */}
            {Array.from({ length: Math.max(0, 3 - orderItems.length) }).map((_, idx) => (
              <tr key={`empty-${idx}`} className="bg-white">
                {Array.from({ length: 9 }).map((_, colIdx) => (
                  <td key={colIdx} className="border border-gray-400 px-1 py-1">&nbsp;</td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Add Row Button */}
      <button
        onClick={addNewRow}
        className="px-3 py-1 bg-gray-200 border border-gray-400 text-sm hover:bg-gray-300"
      >
        + Add Item
      </button>

      {/* Totals */}
      <div className="flex justify-end">
        <div className="space-y-2">
          <div className="flex items-center gap-4">
            <label className="w-20 text-sm text-gray-700 text-right">Total Excl</label>
            <input
              type="text"
              value={totalExcl.toFixed(2)}
              readOnly
              className="w-32 border border-gray-400 px-2 py-1 text-sm bg-gray-50"
            />
          </div>
          <div className="flex items-center gap-4">
            <label className="w-20 text-sm text-gray-700 text-right">Total Tax</label>
            <input
              type="text"
              value={totalTax.toFixed(2)}
              readOnly
              className="w-32 border border-gray-400 px-2 py-1 text-sm bg-gray-50"
            />
          </div>
          <div className="flex items-center gap-4">
            <label className="w-20 text-sm text-gray-700 text-right">Total Incl</label>
            <input
              type="text"
              value={totalIncl.toFixed(2)}
              readOnly
              className="w-32 border border-gray-400 px-2 py-1 text-sm bg-gray-50"
            />
          </div>
        </div>
      </div>
    </div>
  )
}
