import React, { FC, PropsWithChildren, memo } from 'react'
import { useNavigationMemory } from './useNavigationMemory'

const Common: FC = (props) => {
  useNavigationMemory()

  return null
}

export default memo(Common)
