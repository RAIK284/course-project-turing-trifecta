// React
import { useMemo } from "react";
import resolveConfig from "tailwindcss/resolveConfig";
import tailwindConfig from "../../tailwind.config.ts";

/**
 * Hook that allows users access to tailwind's config, as defined in tailwind.config.ts
 * Taken from https://stackoverflow.com/a/77346945
 * @returns
 */
export default function useTailwind() {
  const tailwind = useMemo(
    () => resolveConfig(tailwindConfig),
    [tailwindConfig]
  );

  return tailwind;
}
