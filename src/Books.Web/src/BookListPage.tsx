import { useEffect, useState } from 'react'
import { listBooks, type Book } from './api/books'

export function BookCover() {
  return (
    <div className="book-cover" aria-hidden="true">
      📖
    </div>
  )
}

export function BookCard({ book }: { book: Book }) {
  return (
    <a className="book-card" href={`?id=${book.id}`}>
      <BookCover />
      <div className="book-card-info">
        <p className="book-title">{book.title}</p>
        <p className="book-author">{book.author}</p>
      </div>
    </a>
  )
}

export function BookListPage() {
  const [books, setBooks] = useState<Book[]>([])

  useEffect(() => {
    listBooks().then(setBooks).catch(() => setBooks([]))
  }, [])

  return (
    <>
      <h1>Books</h1>
      <div className="book-grid">
        {books.map((b) => (
          <BookCard key={b.id} book={b} />
        ))}
      </div>
    </>
  )
}
