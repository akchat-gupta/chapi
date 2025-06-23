import { useEffect, useState } from "react";

const SWAGGER_URL = "https://localhost:7112/swagger/v1/swagger.json";
const STORAGE_KEY = "cachedSwaggerSpec";

export const useSwaggerSpec = () => {
  const [swaggerSpec, setSwaggerSpec] = useState<any | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchSwagger = async () => {
      try {
        const res = await fetch(SWAGGER_URL);
        const json = await res.json();
        setSwaggerSpec(json);
        localStorage.setItem(STORAGE_KEY, JSON.stringify(json));
        setError(null);
      } catch (err: any) {
        const cached = localStorage.getItem(STORAGE_KEY);
        if (cached) {
          setSwaggerSpec(JSON.parse(cached));
          setError("Using cached Swagger schema due to network error.");
        } else {
          setError(
            "Failed to fetch Swagger spec and no cached version available."
          );
        }
      } finally {
        setLoading(false);
      }
    };

    fetchSwagger();
  }, []);

  return { swaggerSpec, loading, error };
};
