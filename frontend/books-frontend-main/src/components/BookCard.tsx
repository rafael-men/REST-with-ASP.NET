
import { Book } from "@/types/BookTypes";
import { Card, CardContent, CardFooter } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Link } from "react-router-dom";

interface BookCardProps {
  book: Book;
}

export default function BookCard({ book }: BookCardProps) {
 

  const defaultCover = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2730&q=80";

  return (
    <Link to={`/books/${book.id}`}>
      <Card className="h-full book-card hover:border-primary/50">
        <div className="aspect-[2/3] relative overflow-hidden rounded-t-lg">
          <img 
            src={book.image || defaultCover} 
            alt={`Capa de ${book.title}`}
            className="w-full h-full object-cover transition-transform duration-200 hover:scale-105"
          />
        </div>
        
        <CardContent className="pt-4">
          <h3 className="font-bold text-lg line-clamp-1">{book.title}</h3>
          <p className="text-muted-foreground line-clamp-1">{book.author}</p>
        </CardContent>
      </Card>
    </Link>
  );
}
