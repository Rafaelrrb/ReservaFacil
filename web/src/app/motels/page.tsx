'use client'
import { Header } from '../_components/header'
import { useEffect, useRef, useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/navigation'
import { Suite } from '../suites/page'

interface Motel {
  id: string
  name: string
  address: string
  phone: string
  suites: Suite[]
}

export default function Motels() {
  const router = useRouter()
  const [motels, setMotels] = useState<Motel[]>([])
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

    const fetchMotels = async () => {
      try {
        const response = await axios.get<Motel[]>('http://localhost:5185/motel')
        console.log('Motels:', response.data)
        setMotels(response.data)
      } catch (error) {
        console.error('Error fetching motels:', error)
      }
    }

    fetchMotels()
  }, [router])

  return (
    <>
      <Header href="motels" title="Lista de Motéis" />
      <div className="flex flex-col justify-center items-center min-h-screen p-4 bg-gray-100">
        <h1 className="text-2xl font-bold mb-4">Motels</h1>
        {motels.length === 0 ? ( // Se estiver vazio, mostra uma mensagem
          <p className="text-gray-500">
            Carregando ou nenhum motel encontrado...
          </p>
        ) : (
          motels.map(motel => (
            <ul
              key={motel.id}
              className="bg-white shadow-md rounded-lg p-4 mb-4 w-full max-w-md"
            >
              <button
                onClick={() => {
                  // Enviar dados para Local Storage
                  localStorage.setItem(
                    'suitesData',
                    JSON.stringify(motel.suites)
                  )
                  // Redirecionar para a página de suítes
                  router.push(`/suites`)
                }}
              >
                <li>
                  <p className="text-lg font-semibold">Motél: {motel.name}</p>
                  <p className="text-gray-700">Rua: {motel.address}</p>
                  <p className="text-gray-700">Telefone: {motel.phone}</p>
                </li>
              </button>
            </ul>
          ))
        )}
      </div>
    </>
  )
}
