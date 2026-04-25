import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { customersService } from '../../services/customersService'

export interface Customer {
  clientId: number
  name: string
  address1: string
  address2: string
  address3: string
  suburb: string
  state: string
  postCode: string
}

interface CustomersState {
  customers: Customer[]
  loading: boolean
  error: string | null
}

const initialState: CustomersState = {
  customers: [],
  loading: false,
  error: null,
}

export const fetchCustomers = createAsyncThunk('customers/fetchCustomers', async () => {
  const response = await customersService.getCustomers()
  return response
})

const customersSlice = createSlice({
  name: 'customers',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCustomers.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(fetchCustomers.fulfilled, (state, action) => {
        state.loading = false
        state.customers = action.payload
      })
      .addCase(fetchCustomers.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message || 'Failed to fetch customers'
      })
  },
})

export default customersSlice.reducer
