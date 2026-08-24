import { useEffect, useState } from 'react'
import { listBooks, type Book } from './api/books'

export function BookCover() {
  return (
    <div className="book-cover" aria-hidden="true">
      📖
    </div>
  )
}

export function BookCard({
  book,
  onSelect,
}: {
  book: Book
  onSelect: (id: string) => void
}) {
  return (
    <a
      className="book-card"
      href={`?id=${book.id}`}
      onClick={(event) => {
        if (
          event.button !== 0 ||
          event.metaKey ||
          event.ctrlKey ||
          event.shiftKey ||
          event.altKey
        ) {
          return
        }
        event.preventDefault()
        onSelect(String(book.id))
      }}
    >
      <BookCover />
      <div className="book-card-info">
        <p className="book-title">{book.title}</p>
        <p className="book-author">{book.author}</p>
      </div>
    </a>
  )
}

export function BookListPage({
  onSelect,
}: {
  onSelect: (id: string) => void
}) {
  const [books, setBooks] = useState<Book[]>([])

  useEffect(() => {
    listBooks().then(setBooks).catch(() => setBooks([]))
  }, [])

  return (
    <>
      <h1>Books</h1>
      <div className="book-grid">
        {books.map((b) => (
          <BookCard key={b.id} book={b} onSelect={onSelect} />
        ))}
      </div>
    </>
  )
}
