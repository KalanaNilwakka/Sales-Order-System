import api from './api'
import { Order } from '../redux/slices/ordersSlice'

export const ordersService = {
  getOrders: async (): Promise<Order[]> => {
    try {
      const response = await api.get('/order')
      return response.data
    } catch (error) {
      console.error('Error fetching orders:', error)
      // Return empty array for demo purposes when backend is not available
      return []
    }
  },

  getOrderById: async (id: string): Promise<Order> => {
    const response = await api.get(`/order/${id}`)
    return response.data
  },

  saveOrder: async (order: Order): Promise<Order> => {
    try {
      const response = await api.post('/order', order)
      return response.data
    } catch (error) {
      console.error('Error saving order:', error)
      // Return the order for demo purposes
      return order
    }
  },

  updateOrder: async (order: Order): Promise<Order> => {
    try {
      const response = await api.put(`/order/${order.id}`, order)
      return response.data
    } catch (error) {
      console.error('Error updating order:', error)
      return order
    }
  },

  printOrder: async (orderId: string): Promise<Blob> => {
    const response = await api.get(`/order/${orderId}/pdf`, {
      responseType: 'blob',
    })
    return response.data
  },
}
