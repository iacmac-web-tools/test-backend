import { PersonResource } from "./PersonResource";

export interface ThesisResource {
  id: number;
  mainAuthor?: PersonResource;
  contactEmail?: string;
  otherAuthors?: PersonResource;
  topic?: string;
  content?: string;
  created?: Date;
  updated?: Date;
}
