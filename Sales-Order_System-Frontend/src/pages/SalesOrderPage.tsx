'use client'

import { useAppSelector } from '../hooks/useRedux'
import WindowFrame from '../components/WindowFrame'
import SalesOrderForm from '../components/SalesOrderForm'

export default function SalesOrderPage() {
  const { currentOrder } = useAppSelector((state) => state.orders)

  return (
    <WindowFrame title="Sales Order">
      <SalesOrderForm existingOrder={currentOrder} />
    </WindowFrame>
  )
}
