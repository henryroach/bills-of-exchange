import React, { CSSProperties, FC, memo, useEffect, useState } from 'react'
import { EndorsementDto } from './api'
import { billsOfExchangeApi } from './billOfExchangeService'
import EndorsementCard from './EndorsementCard'
import { showError } from './messageService'
import { Spinner } from './Spinner'
import { handleHttpError } from './utils'

interface IProps {
  billId: number
}

const styles: { row: CSSProperties; card: CSSProperties; arrow: CSSProperties } = {
  row: { height: '100%', overflowX: 'auto', display: 'flex' },
  card: { width: 250, float: 'left' },
  arrow: { fontSize: 50, marginTop: 55 },
}

const Endorsements: FC<IProps> = ({ billId }) => {
  const [data, setData] = useState<EndorsementDto[] | null>(null)
  useEffect(() => {
    const load = async () => {
      try {
        const response = await billsOfExchangeApi.billsOfExchangeGetEndorsementBillIdGet(billId)
        if (response.status !== 200) {
          const errorMessage = `Api call failed with code ${response.status} and ${response.statusText}`
          showError(errorMessage, () => setData([]))
          console.warn(errorMessage)
        }

        setData(response.data.reverse())
      } catch (error) {
        handleHttpError(error, () => setData([]))
      }
    }
    load()
  }, [billId])

  if (!data) {
    return <Spinner />
  }

  return (
    <div style={styles.row}>
      {data.map((e, i) => (
        <EndorsementCard key={i} endorsement={e} current={i === 0} />
      ))}
    </div>
  )
}

export default memo(Endorsements)
