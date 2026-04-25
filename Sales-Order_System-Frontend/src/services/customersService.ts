import api from './api'
import { Customer } from '../redux/slices/customersSlice'

// Mock customers data for demo
const mockCustomers: Customer[] = [
  {
    clientId: 1,
    name: 'ABC Company Ltd',
    address1: '123 Main Street',
    address2: 'Suite 100',
    address3: 'Building A',
    suburb: 'Sydney',
    state: 'NSW',
    postCode: '2000',
  },
  {
    clientId: 2,
    name: 'XYZ Industries',
    address1: '456 Business Ave',
    address2: 'Level 5',
    address3: '',
    suburb: 'Melbourne',
    state: 'VIC',
    postCode: '3000',
  },
  {
    clientId: 3,
    name: 'Tech Solutions Pty',
    address1: '789 Tech Park',
    address2: '',
    address3: '',
    suburb: 'Brisbane',
    state: 'QLD',
    postCode: '4000',
  },
]

export const customersService = {
  getCustomers: async (): Promise<Customer[]> => {
    try {
      const response = await api.get('/client')
      return response.data
    } catch (error) {
      console.error('Error fetching customers:', error)
      // Return mock data for demo purposes
      return mockCustomers
    }
  },

  getCustomerById: async (id: number): Promise<Customer | undefined> => {
    try {
      const response = await api.get(`/client/${id}`)
      return response.data
    } catch (error) {
      console.error('Error fetching customer:', error)
      return mockCustomers.find(c => c.id === id)
    }
  },
}
