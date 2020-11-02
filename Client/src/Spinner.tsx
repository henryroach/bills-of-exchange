import { Card, Spin } from 'antd'
import React, { FC } from 'react'

interface IProps {
  title?: string
}

const card = {
  height: '100%',
}

export const Spinner: FC<IProps> = ({ title }) => (
  <Card title={title} style={card}>
    <Spin size="large"></Spin>
  </Card>
)
