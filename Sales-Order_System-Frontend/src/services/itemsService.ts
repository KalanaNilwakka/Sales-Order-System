import api from './api'
import { Item } from '../redux/slices/itemsSlice'

// Mock items data for demo
const mockItems: Item[] = [
  {
    id: '1',
    itemCode: 'ITEM001',
    description: 'Widget A - Standard',
    price: 25.00,
  },
  {
    id: '2',
    itemCode: 'ITEM002',
    description: 'Widget B - Premium',
    price: 45.50,
  },
  {
    id: '3',
    itemCode: 'ITEM003',
    description: 'Gadget X - Basic',
    price: 15.00,
  },
  {
    id: '4',
    itemCode: 'ITEM004',
    description: 'Gadget Y - Advanced',
    price: 75.00,
  },
  {
    id: '5',
    itemCode: 'ITEM005',
    description: 'Service Package - Monthly',
    price: 100.00,
  },
]

export const itemsService = {
  getItems: async (): Promise<Item[]> => {
    try {
      const response = await api.get('/Item')
      return response.data
    } catch (error) {
      console.error('Error fetching items:', error)
      // Return mock data for demo purposes
      return mockItems
    }
  },

  getItemById: async (id: string): Promise<Item | undefined> => {
    try {
      const response = await api.get(`/Item/${id}`)
      return response.data
    } catch (error) {
      console.error('Error fetching item:', error)
      return mockItems.find(i => i.id === id)
    }
  },
}
