export const calculateExclAmount = (quantity: number, price: number): number => {
  return quantity * price
}

export const calculateTaxAmount = (exclAmount: number, taxRate: number): number => {
  return exclAmount * (taxRate / 100)
}

export const calculateInclAmount = (exclAmount: number, taxAmount: number): number => {
  return exclAmount + taxAmount
}

export const formatCurrency = (amount: number): string => {
  return amount.toFixed(2)
}

export const generateId = (): string => {
  return Date.now().toString(36) + Math.random().toString(36).substr(2)
}

export const formatDate = (date: Date): string => {
  return date.toISOString().split('T')[0]
}
