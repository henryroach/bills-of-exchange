import { message } from 'antd'
import { MessageType } from 'antd/lib/message'

export const showError = (errorMessage: string, callback?: () => void): MessageType =>
  message.error(errorMessage, 5, callback)
