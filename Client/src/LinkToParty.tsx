import React, { FC, memo } from 'react'
import { Link } from 'react-router-dom'

interface IProps {
  title?: string | null
  path: string
}

const LinkToParty: FC<IProps> = ({ title = 'Party', path }) => <Link to={path}>{title}</Link>

export default memo(LinkToParty)
