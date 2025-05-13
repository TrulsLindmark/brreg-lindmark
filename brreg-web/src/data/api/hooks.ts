import { useQuery } from "@tanstack/react-query";
import { queryCompanySummary } from "./queries";

export function useFetchCompanySummary() {
  return useQuery({
    ...queryCompanySummary(),
  });
}
