import React, { FC } from 'react'
import { Link } from 'react-router-dom'
import { Menu } from 'antd'
import { IRouteConfig } from './routes'

interface IProps {
  routes: IRouteConfig[]
}

export const AppMenu: FC<IProps> = ({ routes }) => (
  <Menu mode="horizontal">
    {routes
      .filter((r) => !!r.title)
      .map((r) => (
        <Menu.Item key={r.path}>
          <Link to={r.path}>{r.title}</Link>
        </Menu.Item>
      ))}
  </Menu>
)
