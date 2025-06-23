export interface ApiCall {
  method: string;
  url: string;
  body?: any;
}

export interface LlmMessage {
  role: 'system' | 'user' | 'assistant';
  content: string;
}