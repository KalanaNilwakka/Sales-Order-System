'use client'

import { Order } from '../redux/slices/ordersSlice'

interface OrdersTableProps {
  orders: Order[]
  onRowDoubleClick: (order: Order) => void
}

export default function OrdersTable({ orders, onRowDoubleClick }: OrdersTableProps) {
  const columns = [
    { key: 'clientId', label: 'ClientID' },
    { key: 'invoiceNo', label: 'InvoiceNo' },
    { key: 'invoiceDate', label: 'InvoiceDate' },
    { key: 'referenceNo', label: 'ReferenceNo' },
    { key: 'totalExcl', label: 'TotalExcl' },
    { key: 'totalTax', label: 'TotalTax' },
    { key: 'totalIncl', label: 'TotalIncl' },
  ]

  return (
    <div className="border border-gray-400 overflow-auto">
      <table className="w-full border-collapse text-sm">
        <thead>
          <tr className="bg-gray-300">
            {columns.map((col) => (
              <th
                key={col.key}
                className="border border-gray-400 px-3 py-2 text-left font-medium text-gray-800"
              >
                <span className="text-blue-600">▼</span> {col.label}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {orders.length === 0 ? (
            // Show empty rows when no data
            Array.from({ length: 8 }).map((_, idx) => (
              <tr key={idx} className={idx % 2 === 0 ? 'bg-white' : 'bg-gray-50'}>
                {columns.map((col) => (
                  <td key={col.key} className="border border-gray-400 px-3 py-2 text-gray-600">
                    &quot;
                  </td>
                ))}
              </tr>
            ))
          ) : (
            orders.map((order, idx) => (
              <tr
                key={order.id}
                className={`cursor-pointer hover:bg-blue-100 ${idx % 2 === 0 ? 'bg-white' : 'bg-gray-50'}`}
                onDoubleClick={() => onRowDoubleClick(order)}
              >
                <td className="border border-gray-400 px-3 py-2">{order.clientId}</td>
                <td className="border border-gray-400 px-3 py-2">{order.invoiceNo}</td>
                <td className="border border-gray-400 px-3 py-2">{order.invoiceDate}</td>
                <td className="border border-gray-400 px-3 py-2">{order.referenceNo}</td>
                <td className="border border-gray-400 px-3 py-2">{order.totalExcl.toFixed(2)}</td>
                <td className="border border-gray-400 px-3 py-2">{order.totalTax.toFixed(2)}</td>
                <td className="border border-gray-400 px-3 py-2">{order.totalIncl.toFixed(2)}</td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  )
}
