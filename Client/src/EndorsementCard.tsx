import { CaretLeftOutlined } from '@ant-design/icons'
import { Card, message } from 'antd'
import React, { CSSProperties, FC, memo, useEffect, useMemo, useState } from 'react'
import { EndorsementDto, PartyDto } from './api'
import LinkToParty from './LinkToParty'
import { showError } from './messageService'
import { partiesApi } from './partiesService'
import { getPath, PathIds } from './routes'
import { Spinner } from './Spinner'
import { handleHttpError } from './utils'

interface IProps {
  endorsement: EndorsementDto
  current?: boolean
}

const styles: {
  endorsementCard: CSSProperties
  currentCard: CSSProperties
  card: CSSProperties
  arrow: CSSProperties
} = {
  endorsementCard: { display: 'flex' },
  card: { width: 250, height: 170 },
  currentCard: { borderColor: '#1890ff', fontWeight: 600 },
  arrow: { fontSize: 50, marginTop: 55 },
}

const EndorsementCard: FC<IProps> = ({ endorsement: e, current }) => {
  const [data, setData] = useState<PartyDto | null>(null)
  const cardStyle = useMemo<CSSProperties>(() => {
    return {
      ...styles.card,
      ...(current ? styles.currentCard : {}),
    }
  }, [current])
  useEffect(() => {
    const load = async () => {
      try {
        const response = await partiesApi.partyGetByIdsGet([e.newBeneficiary ?? 0])
        if (response.status !== 200) {
          const errorMessage = `Api call failed with code ${response.status} and ${response.statusText}`
          showError(errorMessage, () => setData({}))
          console.warn(errorMessage)
        }

        setData(response.data[0])
      } catch (error) {
        handleHttpError(error, () => setData({}))
      }
    }
    load()
  }, [e.newBeneficiary])

  if (!data) {
    return <Spinner />
  }

  return (
    <div style={styles.endorsementCard}>
      <Card title={`Endorsement ${e.id}`} style={cardStyle}>
        <p>{`${current ? 'Current' : 'Previous'} beneficiary`}</p>
        <LinkToParty title={data.name} path={getPath(PathIds.detailsOfParty, { id: data.id ?? 0 })} />
      </Card>
      {e.previousEndorsementId && <CaretLeftOutlined style={styles.arrow} title={'Handed over to'} />}
    </div>
  )
}

export default memo(EndorsementCard)
