import { BillsOfExchangeApiFactory } from './api'
import { baseApiUrl } from './config'

export const billsOfExchangeApi = BillsOfExchangeApiFactory(undefined, baseApiUrl)
