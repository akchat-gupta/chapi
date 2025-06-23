export interface Message {
  sender: 'user' | 'llm' | 'api' | 'error';
  text: string;
}