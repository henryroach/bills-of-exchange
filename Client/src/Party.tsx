import React from 'react'
import { useParams } from 'react-router'

interface IPartyParams {
    id: string
}

export const Party = () => {
    const { id } = useParams<IPartyParams>()
    return <>Party: {id}</>
}