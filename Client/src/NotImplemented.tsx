import React, { FC, memo, CSSProperties } from 'react'
import { StopOutlined } from '@ant-design/icons'
import { Card } from 'antd'
import { useRouteMatch } from 'react-router'
import { routes } from './routes'

const iconStyle: CSSProperties = { fontSize: 125, margin: 80, color: 'rgb(9, 109, 217)' }
const h2Style: CSSProperties = { color: 'rgb(9, 109, 217)' }
const cardStyle: CSSProperties = { textAlign: 'center', height: '100%', minHeight: 410 }

const findTitle = (path: string): string | undefined => routes.find((r) => r.path == path)?.title

const NotImplemented: FC = () => {
  const { path } = useRouteMatch<{ path: string }>()
  const title = findTitle(path)

  return (
    <Card title={title} style={cardStyle}>
      <StopOutlined style={iconStyle} />
      <h2 style={h2Style}>Not implemented - use a different item from the menu.</h2>
    </Card>
  )
}

export default memo(NotImplemented)
