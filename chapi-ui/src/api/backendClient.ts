import axios from "axios";

export const callApi = async (
  method: string,
  url: string,
  body?: Record<string, unknown>
): Promise<unknown> => {
  const fullUrl = `https://localhost:7112${url}`;
  const res = await axios({ method, url: fullUrl, data: body });
  return res.data;
};
