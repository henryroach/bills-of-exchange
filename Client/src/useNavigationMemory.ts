// use effect a window location

import { useEffect } from 'react'
import { useHistory, useLocation } from 'react-router'

const storageItem = 'lastLocation'

export const useNavigationMemory = () => {
  const location = useLocation()
  const history = useHistory()
  //const { location } = window

  useEffect(() => {
    const stored = localStorage.getItem(storageItem)
    if (!!stored) {
      history.push({
        pathname: stored,
      })
    }
  }, [])

  useEffect(() => {
    localStorage.setItem(storageItem, location.pathname)
  }, [location.pathname])
}
