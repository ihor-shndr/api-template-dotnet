import { useEffect, useState } from 'react'
import { getBook, type Book } from './api/books'
import { BookCover } from './BookListPage'

export function BookDetailsPage({
  id,
  onBack,
}: {
  id: string
  onBack: () => void
}) {
  const [book, setBook] = useState<Book | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let cancelled = false
    setLoading(true)
    setError(null)
    setBook(null)

    getBook(id)
      .then((result) => {
        if (!cancelled) setBook(result)
      })
      .catch(() => {
        if (!cancelled) setError('Book not found')
      })
      .finally(() => {
        if (!cancelled) setLoading(false)
      })

    return () => {
      cancelled = true
    }
  }, [id])

  return (
    <div className="book-details">
      <button type="button" className="back-link" onClick={onBack}>
        ← Back to list
      </button>

      {loading && <p>Loading...</p>}
      {!loading && error && <p className="error-text">{error}</p>}
      {!loading && !error && book && (
        <div className="book-details-content">
          <div className="book-details-cover">
            <BookCover />
          </div>
          <div className="book-card-info">
            <p className="book-title">{book.title}</p>
            <p className="book-author">{book.author}</p>
            <p className="book-created-date">
              Added {new Date(book.createdDate).toLocaleDateString()}
            </p>
          </div>
        </div>
      )}
    </div>
  )
}
