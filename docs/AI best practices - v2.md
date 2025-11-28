
## TL;DR on Recommended Use Cases

**Information Retrieval** (Just ask questions):
- **Understanding a New Codebase** - Get instant overviews of unfamiliar code, trace dependencies, and understand function behavior
- **PR Review Assistance** - Identify potential bugs, missing test coverage, and security vulnerabilities in pull requests
- **Accelerating QA Workflows** - Query API specifications, generate test scenarios, and understand implementation details

**Quick Wins:**
- **Daily Autocomplete** - Write descriptive comments and let Copilot generate implementations
- **Targeted Bug Fixes** - Make surgical code changes with minimal impact to surrounding logic
- **Small Feature Additions** - Add properties, validation, pagination, etc. to existing code

**Mid-Size Features:**
- **Writing Tests** - Generate test coverage for unit, integration, and edge-case scenarios
- **Using Agents.md** - Create a document with standarts to align AI output with team practices
- **Planning Mode** - Generate implementation plans for heavy tasks before coding

**Complex tasks:**
- **Multi-Phase Implementation** - Break large refactors into phases
- **Rapid Prototyping** - Generate proof-of-concept code quickly but iteratively

---
# Working with Copilot: A Practical Guide for Your Team by Empeek


We all know what an LLM is at its core—a sophisticated next-token prediction engine that's fundamentally non-deterministic. This means AI coding assistants work differently than traditional tools you might be used to. Think of Copilot as a highly capable junior engineer who has read millions of codebases and responds instantly, bringing that vast knowledge to every interaction. However, like any junior developer, it has limitations worth understanding upfront.

Copilot doesn't automatically know the context that lives in your team's collective memory—what you decided in Slack last month, which architectural patterns you've learned to avoid, or what level of code quality is appropriate for different parts of your system. It won't inherently understand your product's business logic or distinguish between a quick prototype and production-critical code.

We highly recommend reading the [**tessl.io AI Best Practices**](./Best-practices_Tessl-Docs.pdf). We really like it as it captures the essential mindset for working effectively with AI. Consider it required reading, but not a rigid rulebook. Adapt its principles to fit how your team actually delivers software.


We're starting with **GitHub Copilot Pro** as our baseline tool. It's widely adopted, deeply integrated into our existing workflows, and familiar to many of you. **GitHub Copilot** is supported across all popular IDEs — Visual Studio, VS Code, JetBrains Rider, IntelliJ IDEA, and more. This means you can use it in your existing development environment without switching tools or learning new interfaces.

**About the models:** Copilot works with two categories of models:

- **Included models (unlimited)**: GPT-5 mini, GPT-4.1, GPT-4o - these are available for unlimited chat and autocomplete
- **Premium models (300 requests/month)**: Gemini 2.5 Pro, Claude Sonnet 4.5, and other advanced models - these consume your monthly premium request allowance

The included models handle everyday coding tasks very well. Premium models are worth using when you need deeper reasoning for complex refactoring, architectural decisions, or working with large codebases. 

Below are proven use cases from our experience and observations, ordered from simplest to most complex.

---

## Section 1: Information Retrieval  
*These tasks require minimal learning curve—just ask questions.*

### Understanding a New Codebase

When joining a project, avoid spending hours manually tracing dependencies. Open Copilot Chat in your IDE:

> *"What are the main entry points for the billing service? How does it communicate with Stripe?"*

You'll quickly receive a coherent overview—like having a patient colleague who actually remembers how everything fits together. For deeper understanding, highlight a specific function and ask:

> *"Explain the `processRefund` function step-by-step. What edge cases does it handle?"*

Use this as an interactive documentation tool. Consider maintaining a personal `ONBOARDING.md` file where you capture these explanations for future reference.

### PR Review Assistance

In GitHub, activate Copilot on a pull request:

> *"Summarize these changes and identify potential bugs or missing test coverage."*

While not a substitute for human review, it consistently catches obvious issues—such as unhandled null cases or missing error handling. 

### Accelerating QA Workflows

QA team members can reduce dependencies on engineers for code-level questions. Instead of waiting for dev responses you can ask Copilot directly:

> *"What are the possible status codes and response structures for the `/api/v2/users/{id}` endpoint?"*

For test planning:

> *"Generate five edge-case test scenarios for the checkout flow based on this implementation."*

---

## Section 2: Code Generation  
*Complexity increases with task scope and the precision required in prompting.*

### ⭐ **1-Star: Autocomplete and Surgical Fixes**

### Daily Autocomplete Usage  
The most common pattern is straightforward: write a clear, descriptive comment first:

```csharp
// Parse CSV string, validate email columns, return list of user records
public List<UserRecord> ParseUserCsv(string csvData)
{
    // Copilot generates a precise implementation
}
```

If the suggestion is mostly correct, accept it and adjust the remainder manually. Repeatedly cycling through suggestions rarely yields better results than making targeted fixes yourself. (Tip: If you've hit "Tab" and "Ctrl+Z" more than three times on the same line, just write it yourself—it'll be faster, and your keyboard will thank you.)

### Targeted Bug Fixes and Small Modifications  
When you identify a specific issue or need to make a localized change, highlight the relevant code and provide clear instructions:

> *"Fix the loop bounds error. Minimize changes to preserve existing logic."*

This works equally well for small feature additions that don't require architectural changes:

> *"Add a 'LastModified' property to the User entity and map it in all DTOs."*

> *"Add email validation to the registration form and display inline error messages."*

The key is specificity—tell Copilot exactly what needs to change and what constraints to respect. For example:

> *"Add pagination support to the GetUsers endpoint. Keep the existing filtering logic intact. Use skip/take parameters."*

These surgical changes are Copilot's sweet spot. The more context you provide about what should stay the same, the better the result. Always review the generated diff before accepting—sometimes the AI will make reasonable but unintended changes to surrounding code.

**A pragmatic note:** If you find yourself repeatedly rejecting suggestions and fighting with the AI over a simple change, it's often faster to just write it yourself. Where AI truly shines is on work that would take you hours or even days—complex implementations, boilerplate-heavy features, or exploratory coding where you're not entirely sure of the approach yet. Don't spend five minutes getting AI to generate a two-line change you could write in thirty seconds.

### Generating Commit Messages and PR Descriptions  
Stop writing "upd" or "small change" as commit messages. Let Copilot analyze your changes and generate meaningful descriptions. Most IDEs have a built-in sparkle icon (✨) in the source control panel that generates commit messages with one click. Alternatively, ask in chat:

> *"Generate a commit message for my staged changes."*

> *"Create a PR description summarizing the changes in this branch."*

The AI will review your diff and produce descriptive, professional text that explains what changed and why—making your git history actually useful for future reference. This is especially valuable when you've made multiple related changes and need to articulate the overall impact clearly. Also, you can generate PR summary in GitHub.com by clicking on `Copilot Summary` button.

### ⭐⭐ **2-Star: Feature Implementation**

### Writing Tests  
Copilot excels at generating comprehensive test coverage when you provide clear context about what needs testing. The key is being specific about scenarios and expectations:

> *"Generate xUnit tests for the UserService.CreateUser method. Cover happy path, validation failures, and duplicate email scenarios."* (C#)

> *"Write Jest tests for the calculateDiscount function. Test percentage discounts, fixed amount discounts, and edge cases with zero/negative values."* (JavaScript)

For more complex scenarios, describe the behavior you're testing:

> *"Create integration tests for the payment processing endpoint. Mock the Stripe API, test successful charges, declined cards, and network timeouts."*

**Tip:** Let the agent run the tests itself and stay in the loop. Instead of just generating tests, ask it to create and execute them: *"Generate tests for the UserService and run them to verify they pass."* This allows the agent to catch and fix issues autonomously—it'll adjust imports, fix assertion syntax, or handle setup problems without requiring your intervention. You stay hands-off while it iterates until the tests are green.

### Using Agents.md for Quality  
For tasks beyond simple functions, create an `Agents.md` file in your repository root. This file bridges the gap between what the AI can see in your code and what it cannot—the architectural decisions made in design meetings, the coding conventions your team agreed upon, the lessons learned from past incidents, and the quality standards that vary between rapid prototypes and production code.

Think of `Agents.md` as institutional knowledge made explicit. Without it, Copilot will make reasonable but generic choices. With it, the output aligns with your team's actual practices, dramatically reducing the time spent on stylistic corrections and architectural mismatches.

Here's an example for a C# project:

```markdown
# AI Coding Standards

## Architecture Decisions
- We use Clean Architecture with MediatR for CQRS patterns
- Repository pattern is required for data access; no direct DbContext usage in controllers
- Use FluentValidation for all request validation

## Code Style
- Use nullable reference types; enable all compiler warnings
- Prefer async/await patterns; avoid .Result or .Wait()
- Use records for DTOs; prefer immutability where possible
- Follow SOLID principles; constructor injection for all dependencies

## Testing Requirements
- Always include xUnit tests for new business logic
- Use FluentAssertions for test assertions
- Mock external dependencies with NSubstitute
- Integration tests must use Testcontainers; never connect to real databases

## Security & Configuration
- Never hardcode connection strings; use IConfiguration
- All secrets must be in Azure Key Vault references
- API endpoints require authentication by default; opt-out must be explicit

## Naming Conventions
- Controllers: Plural nouns (UsersController, not UserController)
- Services: Verb-based interfaces (IProcessPayment, not IPaymentProcessor)
- Private fields: _camelCase with underscore prefix
```

**Your file can be quite detailed**—include examples of patterns you want to encourage and antipatterns you want to avoid. The more specific, the better the output.

**Two approaches to creating this file:**

1. **Manual creation**: Review your team's style guide, pull request comments, and architectural decision records. Distill them into concise rules.

2. **Agent-assisted generation**: Ask Copilot to analyze your codebase and generate a draft:
   > *"Analyze this codebase and create an Agents.md file that captures our coding patterns, architectural decisions, and testing practices."*

**Critical step:** Always review and refine the generated version! The AI might infer patterns that were accidental, not intentional. (Yes, you used `var` everywhere, but that doesn't mean you want to.)

**Treat Agents.md as a living document**—a first-class citizen of your repository that evolves with your team's practices. Review it during retrospectives, update it when architectural decisions change, and refine it as you discover what works. Think of it as your team's coding standards documentation that actually gets read (because the AI reads it every time it generates code).

The beauty is that modern agents automatically consider this file when generating code—you don't need to explicitly reference it in every prompt. Just keep it current, and the quality improvements happen automatically.

### Planning as a Default Habit  
For non-trivial work, planning is not optional—it's required. Instead of asking for immediate implementation switch to `Plan` mode in Copilot and ask something like this:

> *"Create a plan for adding rate limiting to the GraphQL endpoint. Store it in plans/rate-limit.md."*

Review the plan to ensure it aligns with your architecture and catches potential issues early. For mid-size tasks (those 2-3 hour efforts), it's perfectly fine to review the plan and then immediately ask for implementation:

> *"The plan looks good. Now implement Phase 1 (Redis schema) on a feature branch."*

Fixing a flawed plan requires far less effort than debugging poorly structured code. (Plus, explaining the bug to your team lead is much easier when you can point to a plan that made sense at the time.)

### ⭐⭐⭐ **3-Star: Prototypes and Architectural Exploration**

### Multi-Phase Implementation  
For larger features or refactoring work, break the implementation into distinct phases that you execute and validate sequentially. Start with comprehensive planning:

> *"Create a detailed plan for migrating our authentication system from JWT to OAuth2 with OIDC. Include database schema changes, API modifications, and backward compatibility strategy."*

Review the plan carefully, refine it, then implement phase by phase:

1. *"Implement Phase 1 from the auth migration plan: Create new OAuth tables and repository layer."*
2. Review and test Phase 1 thoroughly
3. *"Implement Phase 2: Add OAuth endpoints while maintaining JWT support."*
4. Review and test Phase 2
5. Continue through remaining phases

This approach keeps each AI session focused and manageable, allows you to catch architectural issues early, and ensures each layer is solid before building on top of it. The key difference from 2-star work is that you're deliberately pacing the implementation rather than asking for everything at once and hoping for the best.

### Rapid Prototyping  
Copilot can generate functional proof-of-concept code quickly. For example:

> *"Create a basic Slack bot with Express and minimal OAuth integration."* (JavaScript)

> *"Create a minimal API endpoint with JWT authentication and Swagger docs."* (C#)

> *"Build a React component that visualizes real-time WebSocket data with Chart.js."* (TypeScript)

**Tip:** For prototypes, it's often faster to draft empty interfaces and class signatures yourself rather than explaining your naming conventions to the LLM. Write the scaffolding (interface names, method signatures, basic structure), then let Copilot fill in the implementation details. This saves back-and-forth and ensures the code follows your team's patterns from the start.

**Critical caveat:** The output will not be production-ready. It typically lacks comprehensive error handling, logging, security considerations, and scalability patterns. You must remain actively engaged. Proceed iteratively:

1. *"Create a Slack bot skeleton with OAuth."*
2. *"Add Winston logging and centralized error handling."*
3. *"Write unit tests for the slash command handler."*

Or for an API endpoint (C#):

1. *"Create a Web API controller with basic CRUD operations."*
2. *"Add Serilog structured logging and global exception handling."*
3. *"Write xUnit tests with FluentAssertions for the repository layer."*

Review and validate each step before proceeding. Without disciplined oversight, prototypes become technical debt. (Translation: that "quick spike" you did on Friday will somehow become the production system by Monday. We've all been there.)

**When Copilot Reaches Its Limits**  
For large-scale refactors, architectural overhauls, or spec-driven greenfield projects, Copilot's context window (~128k tokens) becomes restrictive. If you find yourself hitting these limits and feel confident exploring more advanced tools, you might consider:

- **Claude Code** (CLI, Web): Larger context window, excels at repository-wide reasoning and architecture.
- **Cline** (VS Code extension): Large context capacity, can be configured to rely on VS Code Copilot extension. Excellent for heavy tasks but burns tokens insanely fast.
- **Speckit** (prompt engineering framework): A structured prompt library that runs within Copilot, designed for heavy spec-driven development without leaving your IDE.



---

## When NOT to Use AI

Before diving in, know when to step back. AI assistants are not suitable for:

- **Guessing business requirements** — If you don't know what to build, the AI won't either. Clarify requirements with stakeholders first.
- **Security-critical code without review** — Never blindly trust AI-generated authentication, encryption, or authorization logic. Always have a human expert verify.
- **Code you don't understand** — If you can't explain what the generated code does, don't commit it. You own it now.
- **Replacing architectural thinking** — AI can implement patterns, but choosing the right architecture for your context requires human judgment.

---

## Evaluation Tasks for Your Team

During the pilot period, try using Copilot for your regular work. The goal is to get a feel for where it helps and where it doesn't — not to force specific exercises.

**Suggested approach:**

For a fair comparison, you may try this experiment: when you have a small task (a bug fix, a new method), implement it yourself first, then try the same task with Copilot. Compare the results — time spent, code quality, and how much you had to fix.

**Types of tasks we would like you to try:**

**⭐ Small tasks (do 10 of these or more):**
- Fix a small bug or make a minor code change
- Add a property, new validation, or simple feature to existing code
- Ask Copilot to explain unfamiliar code
- Generate commit messages or PR descriptions

**⭐⭐ Mid-Size Tasks (do 5 of these or more):**
- Write tests for existing functionality
- Create and use `Agents.md` for your project
- Use Planning mode before implementing something
- Refactor a method or small module

**⭐⭐⭐ Major Tasks (do 2 of these or more):**
- Implement a feature end-to-end with plan-then-implement workflow
- Build a proof-of-concept for a new app/module
- Migrate legacy code to a newer framework

**How to track:** Keep brief notes — what worked, what didn't, time spent vs. your estimate without AI.

---

## **Most importantly: share your feedback!** 

 Take a moment to reflect. What did you actually like about working with Copilot or other agents? What frustrated you or felt like struggling against a wall? Did it genuinely help, or did it create more work than it saved?

Share your honest thoughts with us. What worked brilliantly for you might become someone else's go-to pattern.This is a learning process for all of us, and your real experience matters more than any theoretical guide.
After completing your evaluation tasks, please answer these questions honestly. Your feedback shapes how we adopt these tools.

---

## Post-Evaluation Survey Questions [(Google Forms link)](https://docs.google.com/forms/d/e/1FAIpQLSedKKN4qHHguoYwif0rUyWBsQSectxV2bCA1lnEW9TaWull2w/viewform?usp=dialog)

### General Experience

**1. How often did you use Copilot during the evaluation period?**
- Never
- A few times total
- A few times per week
- Once a day
- Several times per day
- Almost constantly when coding

**2. On a scale of 1-5, how useful was GitHub Copilot for your daily work?**
- 1 (Not useful, often got in the way)
- 2 (Occasionally helpful)
- 3 (Useful for specific tasks)
- 4 (Very useful, improved my workflow)
- 5 (Game-changer, can't imagine working without it)

**3. Did using Copilot affect your speed?**
- Made me significantly faster
- Made me somewhat faster
- About the same
- Made me somewhat slower
- Made me significantly slower
- Depends on the task (please specify in notes)

**4. What was the single most valuable thing Copilot helped you with?**
- Autocomplete / writing boilerplate
- Fixing bugs
- Writing new features
- Understanding unfamiliar code
- Writing tests
- Generating documentation / commit messages
- Other (please specify)

**5. What was your biggest frustration?** (pick one + explain in notes)
- AI didn't understand our patterns
- AI lacked context (couldn't "see" related files, database schema, etc.)
- AI was too generic (felt like Stack Overflow copy-paste, not our solution)
- AI created more work (generated code I had to rewrite from scratch)
- No major frustrations


### Specific Use Cases
**6. Describe one situation where Copilot clearly helped you** (saved time, effort, or thinking).
- (Open text)

**7. Describe one situation where Copilot clearly failed or caused more work.**
- (Open text)

**8. Which use cases worked best for you?** (select all that apply)
- Bug fixes and small changes
- Refactoring
- Planning features before implementation
- Autocomplete
- Code explanation / understanding
- Writing tests
- Generating commit messages / PR descriptions
- None worked well

**9. Which use cases felt like more trouble than they were worth?** (select all that apply)
- Bug fixes and small changes
- Refactoring
- Planning features before implementation
- Autocomplete
- Code explanation / understanding
- Writing tests
- Generating commit messages / PR descriptions
- All worked fine for me

**10. Did you create or use an Agents.md file?**
- Yes, and it clearly improved output quality (e.g., AI stopped suggesting obsolete patterns)
- Yes, but I couldn't tell if it helped (maybe copied our style guide, but AI still made mistakes)
- No, I didn't have time to create one
- No, I tried but wasn't sure what to put in it

**11. Did you use Planning mode for larger tasks?**
- Used it for a 2+ hour task: It caught issues early and saved time overall
- Used it: It felt like extra paperwork, didn't help
- Didn't use: I tried but the plan was generic/useless
- Didn't use: I didn't have any tasks big enough to need planning

### Comparison & Alternatives

**12. Have you tried any other AI coding tools?** (select all that apply)
- Claude Code
- Cursor
- Codex
- Other (please specify)
- No, only GitHub Copilot

**13. If you tried alternatives, how did they compare to Copilot?**
- Copilot was better overall
- Copilot was worse overall
- About the same
- Different tools work better for different tasks
- N/A — didn't try alternatives

**14. Would you prefer a different tool for certain tasks?**
- No, Copilot covers everything I need
- Yes (please explain for which kinds of tasks)
- Not sure yet

### Moving Forward

**15. Any concerns about AI-assisted development?** (select all that apply)
- Code quality might decrease
- Team becoming over-reliant on AI
- Less learning / skill development
- Harder to review AI-generated code
- Security vulnerabilities in generated code
- No concerns
- Other (please specify)

**16. What would you prefer to do next?**
- Continue with GitHub Copilot as-is
- Continue with Copilot + add training/guidelines
- Try alternative tools
- Switch to a different tool entirely
- Abandon AI tools for now
- Need more evaluation time

**17. What would help you use AI tools more effectively?** (select all that apply)
- More hands-on training sessions
- Better documentation / guidelines
- Pair programming sessions with experienced users
- Example prompts and workflows
- Time dedicated specifically for learning
- Nothing needed, I'm comfortable
- Other (please specify)

**18. Anything else you want to share?**
- (Open text field)