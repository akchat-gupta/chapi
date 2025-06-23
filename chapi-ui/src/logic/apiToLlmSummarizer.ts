import type { LlmMessage } from "@/types/Agent";

export const buildSummaryPrompt = (
  userQuery: string | null,
  apiResponse: unknown
): LlmMessage[] => [
  {
    role: "system",
    content: "You are a helpful assistant that explains API responses clearly.",
  },
  {
    role: "user",
    content:
      `The user asked: "${userQuery}"\n` +
      `The API returned:\n${JSON.stringify(apiResponse, null, 2)}\n` +
      `Give a clear summary of what the API result means in one or two sentences. Do not list each object individually. Focus on overall insights only.`,
  },
];
