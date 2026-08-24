export type Book = {
  id: number
  title: string
  author: string
}

const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5265/api/v1'

export async function getBook(id: number | string): Promise<Book> {
  const response = await fetch(`${API_BASE_URL}/books/${id}`)

  if (!response.ok) {
    throw new Error(
      `Failed to fetch book ${id}: ${response.status} ${response.statusText}`,
    )
  }

  const data = (await response.json()) as { title: string; author: string }

  return {
    id: Number(id),
    title: data.title,
    author: data.author,
  }
}

// Mocked until a real list-books endpoint exists on the backend.
// Only this function's body should need to change once that endpoint ships.
export async function listBooks(): Promise<Book[]> {
  return Promise.resolve([
    { id: 1, title: 'Molloy', author: 'Samuel Beckett' },
    { id: 2, title: 'Malone Dies', author: 'Samuel Beckett' },
    { id: 3, title: 'In Search of Lost Time', author: 'Marcel Proust' },
    { id: 4, title: 'The Unnamable', author: 'Samuel Beckett' },
  ])
}
