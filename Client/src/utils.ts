import { AxiosError } from 'axios'
import { showError } from './messageService'

export const handleHttpError = (error: any, callback: () => void) => {
  const casted = error as AxiosError
  console.error(error)
  showError(`Problem with data: ${casted.response?.data?.split('\n', 1)[0]}`, callback)
}
