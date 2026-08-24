import { useEffect, useState } from 'react'
import './App.css'
import { BookListPage } from './BookListPage'
import { BookDetailsPage } from './BookDetailsPage'

function App() {
  const [id, setId] = useState(
    () => new URLSearchParams(window.location.search).get('id'),
  )

  useEffect(() => {
    const onPopState = () =>
      setId(new URLSearchParams(window.location.search).get('id'))
    window.addEventListener('popstate', onPopState)
    return () => window.removeEventListener('popstate', onPopState)
  }, [])

  const handleBack = () => {
    window.history.pushState({}, '', window.location.pathname)
    setId(null)
  }

  const handleSelect = (selectedId: string) => {
    window.history.pushState({}, '', `?id=${selectedId}`)
    setId(selectedId)
  }

  return (
    <main className="page">
      {id ? (
        <BookDetailsPage id={id} onBack={handleBack} />
      ) : (
        <BookListPage onSelect={handleSelect} />
      )}
    </main>
  )
}

export default App
