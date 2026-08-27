export const meta = {
  name: 'dummy-demo',
  description: 'Dummy demo workflow with two trivial agents',
  phases: [
    { title: 'Greet' },
    { title: 'Combine' },
  ],
}

phase('Greet')
const results = await parallel([
  () => agent('Reply with exactly the word: Hello', { label: 'agent-1' }),
  () => agent('Reply with exactly the word: World', { label: 'agent-2' }),
])

phase
('Combine')
const combined = await agent(`Combine these two words into one greeting sentence: "${results[0]}" and "${results[1]}"`, { label: 'combiner' })

return { parts: results, combined }
