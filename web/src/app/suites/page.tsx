'use client'
import { useEffect, useRef, useState } from 'react'
import { Header } from '../_components/header'
import axios from 'axios'
import { useRouter } from 'next/navigation'

export interface Suite {
  id: string
  numero: string
  idMotel: string
  idTypeSuite: string
  status: string
}
export interface TypeSuite {
  id: string
  description: string
  price: number
}

export default function SuitesPage() {
  const router = useRouter()
  const [suites, setSuites] = useState<Suite[]>([])
  const [typeSuite, setTypeSuite] = useState<TypeSuite[]>([])
  const fetched = useRef(false)

  useEffect(() => {
    const token =
      typeof window !== 'undefined' ? localStorage.getItem('token') : null

    if (!token) {
      router.push('/')
      return
    }

    if (fetched.current) return
    fetched.current = true

    const storedSuites = localStorage.getItem('suitesData')
    if (storedSuites) {
      setSuites(JSON.parse(storedSuites))
    }

    const fetchSuite = async () => {
      try {
        const response = await axios.get<TypeSuite[]>(
          'http://localhost:5185/type'
        )
        console.log('tipos de suite:', response.data)
        setTypeSuite(response.data)
      } catch (error) {
        console.error('Error fetching motels:', error)
      }
    }

    fetchSuite()
  }, [router])

  return (
    <>
      <Header href="motels" title="Lista de Motéis" />
      <div className="flex flex-col justify-center items-center min-h-screen p-4 bg-gray-100">
        <h1 className="text-2xl font-bold mb-4">Lista de Suítes</h1>
        {suites.length > 0 ? (
          suites.map(suite => (
            <ul
              key={suite.id}
              className="bg-white shadow-md rounded-lg p-4 mb-4 w-full max-w-md"
            >
              <li>Número: {suite.numero}</li>
              <li>Status: {suite.status}</li>
              <li>
                Tipo de Suíte:{' '}
                {typeSuite.find(type => type.id === suite.idTypeSuite)
                  ?.description || 'Tipo não encontrado'}
              </li>
              <li>
                Preço:{' '}
                {typeSuite.find(type => type.id === suite.idTypeSuite)?.price ||
                  'Preço não encontrado'}
              </li>
            </ul>
          ))
        ) : (
          <p>Nenhuma suíte encontrada.</p>
        )}
      </div>
    </>
  )
}
