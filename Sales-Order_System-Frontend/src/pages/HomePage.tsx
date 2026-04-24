'use client'

import { useEffect } from 'react'
import { useRouter } from 'next/navigation'
import { useAppDispatch, useAppSelector } from '../hooks/useRedux'
import { fetchOrders, setCurrentOrder, Order } from '../redux/slices/ordersSlice'
import WindowFrame from '../components/WindowFrame'
import OrdersTable from '../components/OrdersTable'

export default function HomePage() {
  const router = useRouter()
  const dispatch = useAppDispatch()
  const { orders } = useAppSelector((state) => state.orders)

  useEffect(() => {
    dispatch(fetchOrders())
  }, [dispatch])

  const handleAddNew = () => {
    dispatch(setCurrentOrder(null))
    router.push('/sales-order')
  }

  const handleRowDoubleClick = (order: Order) => {
    dispatch(setCurrentOrder(order))
    router.push('/sales-order')
  }

  return (
    <WindowFrame title="Home">
      <div className="space-y-4">
        <button
          onClick={handleAddNew}
          className="px-4 py-1 bg-gray-200 border border-gray-400 text-sm hover:bg-gray-300"
        >
          Add New
        </button>
        <OrdersTable orders={orders} onRowDoubleClick={handleRowDoubleClick} />
      </div>
    </WindowFrame>
  )
}
