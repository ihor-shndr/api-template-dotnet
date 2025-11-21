# AI Agent Best Practices for Presales Prototyping

## 1) Before you start — context is everything

The more you invest in the initial prompt, the better. Prepare a **context pack** (problem definition, target audience, technical constraints, APIs/schemas, sample data, glossary). Be explicit and verbose. **More context now = less refactoring later**.

## 2) While building — show, don't tell

Start from a **skeleton app**. We have a starter template with proper layering & error handling—you can use it as a starting point. When you share it with AI, it won't need to guess your architecture. Define **interfaces/types** first; ask the agent to implement against them. Work in **small loops**: Plan → Implement → Review → Test.

## 3) After each step — iterate to demo-ready

**Timebox** your iterations to a single user story. Don't ask AI to do everything in one go—while LLMs can handle complex requests, you won't like the result :) . Also, it will easily become unmanageable as you won't be able to grasp the LLM's output.

## Other useful techniques

* **Memory bank (e.g., Cline-style):** Maintain a living summary of decisions, APIs, edge cases, and TODOs. Ask the agent to **update memory after every loop** and seed it at the start. However, this is more useful when building production-ready apps, not prototyping.

* **Custom rules:** Set upfront rules like: *"After each iteration, run tests and lint; never change public interfaces; return a unified diff only."* This helps AI remember your decisions and maintain consistency.

* **Reset if you get stuck.** This happens quite often when AI goes down the wrong path. Don't struggle with it for too long. It's better to stop and refine the initial prompt to be more precise.

## 🚀 Experiment!

This is a new and exciting field—there are no strict rules yet. If something works for you, do it. Also, share your experience with us.