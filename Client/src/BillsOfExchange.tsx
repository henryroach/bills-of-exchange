import React, { FC, useEffect, useMemo, useState } from 'react'
import { BillOfExchangeDto, BillOfExchangeDtoPagedResultDto, BillsOfExchangeApiFactory } from './api'
import { Table, message, Button } from 'antd'
import { ColumnType, TablePaginationConfig } from 'antd/lib/table'
import { Link } from 'react-router-dom'
import { getPath, PathIds } from './routes'
import { billsOfExchangeApi } from './billOfExchangeService'
import { handleHttpError } from './utils'
import { showError } from './messageService'

const columns: ColumnType<BillOfExchangeDto>[] = [
  {
    title: 'Id',
    key: 'id',
    dataIndex: 'id',
  },
  {
    title: 'Amount',
    key: 'amount',
    dataIndex: 'amount',
  },
  {
    key: 'button',
    render: (_, record) => (
      <Button type="primary">
        <Link to={getPath(PathIds.detailOfBillsOfExchange, { id: record.id?.toString() || '' })}>Detail</Link>
      </Button>
    ),
  },
]

const defaultPageSize = 5

export const BillsOfExchange: FC = () => {
  const [data, setData] = useState<BillOfExchangeDtoPagedResultDto>({})
  const [pageSize, setPageSize] = useState<number>(defaultPageSize)
  const [page, setPage] = useState<number>(0)

  useEffect(() => {
    const loadData = async () => {
      try {
        const response = await billsOfExchangeApi.billsOfExchangeGetGet(pageSize * page, pageSize)
        if (response.status !== 200) {
          const errorMessage = `Api call failed with code ${response.status} and ${response.statusText}`
          showError(errorMessage, () => setData({ data: [] }))
          console.warn(errorMessage)
        }

        setData(response.data)
      } catch (error) {
        handleHttpError(error, () => setData({ data: [] }))
      }
    }

    loadData()
  }, [page, pageSize])
  const { data: items = [], count = 0 } = data

  const pagination = useMemo<TablePaginationConfig>(() => {
    return {
      defaultPageSize: defaultPageSize,
      pageSizeOptions: [defaultPageSize.toString(), '10'],
      showSizeChanger: true,
      total: count,
      onChange: (page1, pageSize1 = defaultPageSize) => {
        if (pageSize !== pageSize1) {
          setPageSize(pageSize1)
        }
        const pageIndex = page1 - 1
        if (page !== pageIndex) {
          setPage(pageIndex)
        }
      },
    }
  }, [count, page, pageSize])

  return (
    <Table
      columns={columns}
      dataSource={items ?? []}
      loading={!data.data}
      rowKey={rowKeyResolver}
      pagination={pagination}
    />
  )
}

const rowKeyResolver = (row: BillOfExchangeDto): React.Key => row.id || 1
