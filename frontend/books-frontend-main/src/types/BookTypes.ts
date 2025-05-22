
export interface Book {
  id: string;
  title: string;
  author: string;
  genre: string;
  description: string;
  coverUrl?: string;
  pages: number;
  publicationYear: number;
  publisher: string;
  isbn: string;
  status: "available" | "borrowed" | "reserved";
  rating?: number;
  createdAt: string;
  updatedAt: string;
}

export interface BookFormData {
  title: string;
  author: string;
  genre: string;
  description: string;
  coverUrl?: string;
  pages: number;
  publicationYear: number;
  publisher: string;
  isbn: string;
  status: "available" | "borrowed" | "reserved";
  rating?: number;
}
