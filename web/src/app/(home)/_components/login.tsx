'use client'
import { FormEvent, useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/navigation'

export default function LoginForm() {
  const router = useRouter()
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [passwordHash, setPasswordHash] = useState('')
  const [action, setAction] = useState('login')

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()

    if (action === 'login') {
      try {
        const response = await axios.post<{ access_token: string }>(
          'http://localhost:5185/user/login',
          {
            name,
            email,
            passwordHash
          },
          {
            headers: {
              'Content-Type': 'application/json'
            }
          }
        )

        if (response.data.access_token !== 'undefined') {
          localStorage.setItem('token', response.data.access_token)
          router.push(`/motels`)
        } else {
          alert('Email ou senha incorretos')
        }
      } catch (error) {
        console.error('Erro ao enviar dados:', error)
      }
    } else {
      try {
        await axios.post(
          'http://localhost:5185/user',
          {
            name,
            email,
            passwordHash
          },
          {
            headers: {
              'Content-Type': 'application/json'
            }
          }
        )
        window.location.reload()
      } catch (error) {
        console.error('Erro ao criar conta:', error)
      }
    }
  }

  return (
    <div className="flex justify-center items-center h-screen ">
      <div className="bg-white p-8 rounded shadow-md">
        <select
          className=" mb-4 shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="action"
          onChange={e => setAction(e.target.value)}
        >
          <option value="login">Login</option>
          <option value="create">Criar Conta</option>
        </select>

        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label
              className="block text-gray-700 text-sm font-bold mb-2"
              htmlFor="name"
            >
              Nome:
            </label>
            <input
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              id="name"
              type="text"
              placeholder="Nome"
              onChange={e => setName(e.target.value)}
            />
          </div>
          <div className="mb-4">
            <label
              className="block text-gray-700 text-sm font-bold mb-2"
              htmlFor="email"
            >
              Email:
            </label>
            <input
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              id="email"
              type="email"
              placeholder="Email"
              onChange={e => setEmail(e.target.value)}
            />
          </div>
          <div className="mb-6">
            <label
              className="block text-gray-700 text-sm font-bold mb-2"
              htmlFor="passwordHash"
            >
              Senha:
            </label>
            <input
              className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
              id="passwordHash"
              type="passwordHash"
              placeholder="**********"
              onChange={e => setPasswordHash(e.target.value)}
            />
          </div>
          <div className="flex items-center justify-between">
            <button
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
              type="submit"
            >
              Entrar
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
