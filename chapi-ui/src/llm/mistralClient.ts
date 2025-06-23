import type { LlmMessage } from "@/types/Agent";

export const callMistral = async (messages: LlmMessage[]): Promise<string> => {
  const res = await fetch("http://localhost:11434/api/chat", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ model: "mistral", messages, stream: false }),
  });
  const data = await res.json();
  return data.message?.content || "";
};
