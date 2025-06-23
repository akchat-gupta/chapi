import type { LlmMessage } from "@/types/Agent";

export const buildOpenApiPrompt = (swagger: object): LlmMessage => ({
  role: "system",
  content:
    "You are an OpenAPI agent. Only use the APIs defined in this OpenAPI spec:\n" +
    JSON.stringify(swagger) +
    '\nIf the user asks for a feature not described, reply: "This module is still in development and not available yet." ' +
    "Otherwise, respond ONLY with a single valid JSON object like:\n" +
    '{"apiCall": {"method": "POST", "url": "/api/Tasks", "body": {}}}',
});
