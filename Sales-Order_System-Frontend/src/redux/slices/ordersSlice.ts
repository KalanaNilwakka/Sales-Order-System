import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit'
import { ordersService } from '../../services/ordersService'

export interface OrderItem {
  id: string
  itemCode: string
  description: string
  note: string
  quantity: number
  price: number
  taxRate: number
  exclAmount: number
  taxAmount: number
  inclAmount: number
}

export interface Order {
  id: string
  clientId: number
  customerName: string
  address1: string
  address2: string
  address3: string
  suburb: string
  state: string
  postCode: string
  invoiceNo: string
  invoiceDate: string
  referenceNo: string
  note: string
  items: OrderItem[]
  totalExcl: number
  totalTax: number
  totalIncl: number
}

interface OrdersState {
  orders: Order[]
  currentOrder: Order | null
  loading: boolean
  error: string | null
}

const initialState: OrdersState = {
  orders: [],
  currentOrder: null,
  loading: false,
  error: null,
}

export const fetchOrders = createAsyncThunk('orders/fetchOrders', async () => {
  const response = await ordersService.getOrders()
  return response
})

export const saveOrder = createAsyncThunk('orders/saveOrder', async (order: Order) => {
  const response = await ordersService.saveOrder(order)
  return response
})

export const updateOrder = createAsyncThunk('orders/updateOrder', async (order: Order) => {
  const response = await ordersService.updateOrder(order)
  return response
})

const ordersSlice = createSlice({
  name: 'orders',
  initialState,
  reducers: {
    setCurrentOrder: (state, action: PayloadAction<Order | null>) => {
      state.currentOrder = action.payload
    },
    clearCurrentOrder: (state) => {
      state.currentOrder = null
    },
    addOrderLocally: (state, action: PayloadAction<Order>) => {
      const existingIndex = state.orders.findIndex(o => o.id === action.payload.id)
      if (existingIndex >= 0) {
        state.orders[existingIndex] = action.payload
      } else {
        state.orders.push(action.payload)
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchOrders.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchOrders.fulfilled, (state, action) => {
        state.loading = false
        state.orders = action.payload
      })
      .addCase(fetchOrders.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to fetch orders'
      })
      .addCase(saveOrder.fulfilled, (state, action) => {
        state.orders.push(action.payload)
      })
      .addCase(updateOrder.fulfilled, (state, action) => {
        const index = state.orders.findIndex(o => o.id === action.payload.id)
        if (index !== -1) {
          state.orders[index] = action.payload
        }
      })
  },
})

export const { setCurrentOrder, clearCurrentOrder, addOrderLocally } = ordersSlice.actions
export default ordersSlice.reducer
