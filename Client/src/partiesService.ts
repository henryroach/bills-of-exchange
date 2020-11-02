import { PartyApiFactory } from './api'
import { baseApiUrl } from './config'

export const partiesApi = PartyApiFactory(undefined, baseApiUrl)
