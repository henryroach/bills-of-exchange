import React, { CSSProperties, FC, memo, useEffect, useState } from 'react'
import { Card, Descriptions } from 'antd'
import { useParams } from 'react-router'
import { BillOfExchangeDetailDto } from './api'
import { billsOfExchangeApi } from './billOfExchangeService'
import { getPath, PathIds } from './routes'
import Endorsements from './Endorsements'
import { Spinner } from './Spinner'
import LinkToParty from './LinkToParty'
import { handleHttpError } from './utils'
import { showError } from './messageService'

const styles: { card: CSSProperties; endorsements: CSSProperties } = {
  card: {
    height: '100%',
  },
  endorsements: {
    width: '100%',
  },
}

const DetailOfBill: FC = () => {
  const { id } = useParams<{ id: string }>()
  const [data, setData] = useState<BillOfExchangeDetailDto>({})

  useEffect(() => {
    const load = async () => {
      try {
        const response = await billsOfExchangeApi.billsOfExchangeGetByIdIdGet(+id)
        if (response.status !== 200) {
          const errorMessage = `Api call failed with code ${response.status} and ${response.statusText}`
          showError(errorMessage, () => setData({}))
          console.warn(errorMessage)
        }

        setData(response.data)
      } catch (error) {
        handleHttpError(error, () => setData({}))
      }
    }
    load()
  }, [id])

  if (!data.id) {
    return <Spinner />
  }

  const { amount, firstBeneficiary, drawer } = data

  return (
    <Card title={`Bill of exchange ${id}`} style={styles.card}>
      <Descriptions column={3}>
        <Descriptions.Item label="Drawer">
          <LinkToParty title={drawer?.name} path={getPath(PathIds.detailsOfParty, { id: drawer?.id || 0 })} />
        </Descriptions.Item>
        <Descriptions.Item label="Amount">{amount}</Descriptions.Item>
        <Descriptions.Item label="First beneficiary">
          <LinkToParty
            title={firstBeneficiary?.name}
            path={getPath(PathIds.detailsOfParty, { id: firstBeneficiary?.id || 0 })}
          />
        </Descriptions.Item>
        <Descriptions.Item label="Endorsements" span={3} style={styles.endorsements}>
          <></>
        </Descriptions.Item>
      </Descriptions>
      <Endorsements billId={+id} />
    </Card>
  )
  return <h1>Bill {id}</h1>
}

export default memo(DetailOfBill)
