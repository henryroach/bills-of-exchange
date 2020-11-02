import React, { FC } from 'react'
import { Redirect } from 'react-router'
import { BillsOfExchange } from './BillsOfExchange'
import DetailOfBill from './DetailOfBill'
import NotImplemented from './NotImplemented'
import { Parties } from './Parties'
import { Party } from './Party'

export enum PathIds {
  listOfBillsOfExchange,
  detailOfBillsOfExchange,
  listOfParties,
  detailsOfParty,
  default,
}

export interface IRouteConfig {
  id: PathIds
  path: string
  title?: string
  component: FC
  exact?: boolean
}

export const routes: IRouteConfig[] = [
  {
    id: PathIds.listOfBillsOfExchange,
    path: '/billsOfExchange',
    component: BillsOfExchange,
    title: 'Bills of exchange',
  },
  { id: PathIds.detailOfBillsOfExchange, path: '/billOfExchange/:id', component: DetailOfBill },
  { id: PathIds.listOfParties, path: '/parties', component: Parties, title: 'Parties' },
  { id: PathIds.detailsOfParty, path: '/party/:id', component: NotImplemented },
  {
    id: PathIds.default,
    path: '/',
    component: () => <Redirect to={getPath(PathIds.listOfBillsOfExchange)} />,
    exact: true,
  },
]

export const getPath = (pathId: PathIds, routeParams?: { [key: string]: string | number }): string => {
  const path = (routes.find((r) => r.id === pathId) ?? routes[0]).path
  if (!routeParams) {
    return path
  }

  let link = path

  for (const routeParam of Object.getOwnPropertyNames(routeParams)) {
    const value = routeParams[routeParam]
    link = link.replace(`:${routeParam}`, value.toString())
  }

  return link
}
