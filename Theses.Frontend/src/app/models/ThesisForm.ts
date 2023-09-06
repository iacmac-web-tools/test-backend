import { PersonResource } from "./PersonResource";

export interface ThesisForm {
  id: number;
  mainAuthor: PersonResource;
  contactEmail: string | null;
  otherAuthors: PersonResource[] | null;
  topic: string;
  content: string | null;
}
