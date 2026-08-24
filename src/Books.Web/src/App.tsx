import { useEffect, useState } from 'react'
import './App.css'
import { getBook, listBooks, type Book } from './api/books'

function App() {
  const [id, setId] = useState(
    () => new URLSearchParams(window.location.search).get('id'),
  )

  const [book, setBook] = useState<Book | null>(null)
  const [bookLoading, setBookLoading] = useState(false)
  const [bookError, setBookError] = useState<string | null>(null)

  const [books, setBooks] = useState<Book[]>([])

  useEffect(() => {
    listBooks().then(setBooks).catch(() => setBooks([]))
  }, [])

  useEffect(() => {
    if (!id) {
      setBook(null)
      setBookError(null)
      return
    }

    let cancelled = false
    setBookLoading(true)
    setBookError(null)

    getBook(id)
      .then((result) => {
        if (!cancelled) setBook(result)
      })
      .catch((error: unknown) => {
        if (!cancelled) {
          setBookError(error instanceof Error ? error.message : String(error))
        }
      })
      .finally(() => {
        if (!cancelled) setBookLoading(false)
      })

    return () => {
      cancelled = true
    }
  }, [id])

  useEffect(() => {
    const onPopState = () =>
      setId(new URLSearchParams(window.location.search).get('id'))
    window.addEventListener('popstate', onPopState)
    return () => window.removeEventListener('popstate', onPopState)
  }, [])

  return (
    <main style={{ maxWidth: 480, margin: '2rem auto', fontFamily: 'sans-serif' }}>
      <h1>Books</h1>

      {id ? (
        <section>
          {bookLoading && <p>Loading book {id}...</p>}
          {bookError && <p style={{ color: 'crimson' }}>Error: {bookError}</p>}
          {book && !bookLoading && !bookError && (
            <div>
              <p>
                <strong>Title:</strong> {book.title}
              </p>
              <p>
                <strong>Author:</strong> {book.author}
              </p>
            </div>
          )}
        </section>
      ) : (
        <p>Add ?id=1 to the URL, or pick a book below.</p>
      )}

      <h2>Books (mock list)</h2>
      <ul>
        {books.map((b) => (
          <li key={b.id}>
            <a href={`?id=${b.id}`}>
              {b.id} - {b.title}
            </a>
          </li>
        ))}
      </ul>
    </main>
  )
}

export default App
