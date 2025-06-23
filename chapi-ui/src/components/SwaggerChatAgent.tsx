import type { LlmMessage } from "@/types/Agent";
import type { Message } from "@/types/Message";
import { jsonrepair } from "jsonrepair";
import React, { useEffect, useState } from "react";
import { buildOpenApiPrompt } from "../agents/openApiAgent";
import { callApi } from "../api/backendClient";
import { useSwaggerSpec } from "../hooks/useSwaggerSpec";
import { callMistral } from "../llm/mistralClient";
import { buildSummaryPrompt } from "../logic/apiToLlmSummarizer";
import type { ApiCall } from '../types/Agent';

export const SwaggerChatAgent: React.FC = () => {
  const { swaggerSpec, loading, error } = useSwaggerSpec();
  const [input, setInput] = useState('');
  const [messages, setMessages] = useState<Message[]>([]);
  const [chatHistory, setChatHistory] = useState<LlmMessage[]>([]);
  const [lastUserInput, setLastUserInput] = useState<string | null>(null);

  useEffect(() => {
    if (swaggerSpec) {
      const systemPrompt = buildOpenApiPrompt(swaggerSpec);
      console.log('[SwaggerChatAgent] Initialized system prompt:', systemPrompt);
      setChatHistory([systemPrompt]);
    }
  }, [swaggerSpec]);

  const sendMessage = async () => {
    if (!input.trim() || !swaggerSpec) return;

    const userMessage = input.trim();
    setLastUserInput(userMessage);
    setInput('');
    setMessages(prev => [...prev, { sender: 'user', text: userMessage }]);

    const updatedHistory: LlmMessage[] = [
      ...chatHistory,
      { role: 'user', content: userMessage }
    ];
    setChatHistory(updatedHistory);

    const rawLlm = await callMistral(updatedHistory);
    console.log('[SwaggerChatAgent] LLM API call response:', rawLlm);

    const apiCall = tryParseApiCall(rawLlm);
    console.log('[SwaggerChatAgent] Parsed API call:', apiCall);
    if (!apiCall) return;

    try {
      console.log(`[SwaggerChatAgent] Sending API request: ${apiCall.method} ${apiCall.url}`, apiCall.body);
      const apiResponse = await callApi(apiCall.method, apiCall.url, apiCall.body);
      console.log('[SwaggerChatAgent] API response payload:', apiResponse);

      const summaryPrompt = buildSummaryPrompt(userMessage, apiResponse);
      const summaryText = await callMistral(summaryPrompt);
      console.log('[SwaggerChatAgent] LLM summary response:', summaryText);

      setMessages(prev => [...prev, { sender: 'llm', text: summaryText }]);
    } catch (err: any) {
      console.error('[SwaggerChatAgent] API Error:', err.message);
      setMessages(prev => [...prev, { sender: 'error', text: `API Error: ${err.message}` }]);
    }
  };

  const tryParseApiCall = (text: string): ApiCall | null => {
    try {
      const repaired = jsonrepair(text);
      const parsed = JSON.parse(repaired);
      if (
        parsed?.apiCall?.method &&
        typeof parsed.apiCall.url === 'string'
      ) {
        return parsed.apiCall;
      }
      return null;
    } catch (err) {
      console.warn('[SwaggerChatAgent] Failed to repair or parse JSON from LLM response:', text);
      return null;
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-6">
      <div className="mb-2 text-sm text-gray-600">
        {loading && 'Loading Swagger...'}
        {error && <span className="text-red-500">{error}</span>}
      </div>
      <div className="h-[65vh] overflow-y-auto border rounded p-4 bg-white shadow">
        {messages.map((msg, idx) => (
          <div key={idx} className={`mb-3 p-2 rounded text-sm ${msg.sender === 'user' ? 'bg-blue-100' : msg.sender === 'llm' ? 'bg-gray-100' : msg.sender === 'api' ? 'bg-green-100' : 'bg-red-100'}`}>
            <strong>{msg.sender.toUpperCase()}:</strong>
            <pre className="whitespace-pre-wrap break-words">{msg.text}</pre>
          </div>
        ))}
      </div>
      <div className="mt-4 flex">
        <input
          className="flex-grow border border-gray-300 rounded-l px-4 py-2 focus:outline-none"
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === 'Enter' && sendMessage()}
          placeholder="Ask something like 'Create a task'"
          disabled={loading || !!error}
        />
        <button
          onClick={sendMessage}
          className="bg-blue-600 text-white px-4 py-2 rounded-r disabled:opacity-50"
          disabled={loading || !!error}
        >
          Send
        </button>
      </div>
    </div>
  );
};
