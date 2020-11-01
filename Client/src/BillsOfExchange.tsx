import React, { FC, useEffect, useState } from 'react'
import { BillOfExchangeDtoPagedResultDto, BillsOfExchangeApiFactory } from './api'
import { Table } from 'antd'
import { baseApiUrl } from './config'

const columns = [
    {
        title: "Id",
        key: "id",
        dataIndex: "id"
    },
    {
        title: "Amount",
        key: "amount",
        dataIndex: "amount"
    }
]

export const BillsOfExchange: FC = () => {
    const [data, setData] = useState<BillOfExchangeDtoPagedResultDto>()

    useEffect(() => {
        const loadData = async () => {
            try {
                const response = await BillsOfExchangeApiFactory({}, baseApiUrl).billsOfExchangeGetGet({})

                if (response.status !== 200) {
                    throw new Error(`Api call failed with code ${response.status} and ${response.statusText}`)
                }

                setData(response.data)
            }
            catch (error) {
                console.error(error)
            }
        }

        loadData()
    }, [])

    return <Table columns={columns} dataSource={data?.data ?? []} loading={!data}  />
}