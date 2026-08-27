export type Book = {
  id: number
  title: string
  author: string
  createdDate: string
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

  const data = (await response.json()) as {
    title: string
    author: string
    createdDate: string
  }

  return {
    id: Number(id),
    title: data.title,
    author: data.author,
    createdDate: data.createdDate,
  }
}

export async function deleteBook(id: number | string): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/books/${id}`, {
    method: 'DELETE',
  })

  if (!response.ok) {
    throw new Error(
      `Failed to delete book ${id}: ${response.status} ${response.statusText}`,
    )
  }
}

// Fetches the known seed ids one by one until a real bulk list-books endpoint
// exists on the backend. This is isolated behind one function, swap for a
// real bulk endpoint later.
const SEED_BOOK_IDS = Array.from({ length: 20 }, (_, index) => index + 1)

export async function listBooks(): Promise<Book[]> {
  const results = await Promise.allSettled(SEED_BOOK_IDS.map(getBook))

  return results
    .filter(
      (result): result is PromiseFulfilledResult<Book> =>
        result.status === 'fulfilled',
    )
    .map((result) => result.value)
    .sort((a, b) => a.id - b.id)
}
