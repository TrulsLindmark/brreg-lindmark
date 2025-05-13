import { queryOptions } from "@tanstack/react-query";
import { ICompanySummary } from "../models/companySummary";

export function queryCompanySummary() {
  return queryOptions({
    queryKey: ["companySummary"],
    queryFn: async () => {
      const response = await fetch("/api/companies/summary");
      console.log(response);
      if (response.ok === false) {
        throw new Error("Network response was not ok");
      }
      const data: ICompanySummary = await response.json();
      return data;
    },
  });
}
