import { useEffect, useState } from "react";
import Navbar from "@/components/Navbar";
import BookStats from "@/components/BookStats";
import BookCard from "@/components/BookCard";
import { Book } from "@/types/BookTypes";
import { Button } from "@/components/ui/button";
import { PlusCircle } from "lucide-react";
import { Link } from "react-router-dom";
import { useToast } from "@/components/ui/use-toast";

export default function Index() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const { toast } = useToast();

  useEffect(() => {
  const fetchBooks = async () => {
    const token = localStorage.getItem("authToken"); 

    if (!token) {
      toast({
        title: "Erro de autenticação",
        description: "Token não encontrado. Faça login novamente.",
        variant: "destructive",
      });
      setLoading(false);
      return;
    }

    try {
      const response = await fetch("http://localhost:5151/v1/api/books", {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Erro ao buscar livros");
      }

      const data = await response.json();

    
      setBooks(Array.isArray(data) ? data : data.books);
    } catch (error) {
      toast({
        title: "Erro",
        description: "Não foi possível carregar os livros",
        variant: "destructive",
      });
    } finally {
      setLoading(false);
    }
  };

  fetchBooks();
}, [toast]);


  const recentBooks = [...books]
    .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
    .slice(0, 3);

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <main className="container mx-auto px-4 py-8 page-transition">
        <div className="flex flex-col md:flex-row md:items-center justify-between mb-8">
          <div>
            <h1 className="text-3xl md:text-4xl font-bold text-gray-900">Biblioteca Digital</h1>
            <p className="text-gray-600 mt-2">Gerencie sua coleção de livros</p>
          </div>
          <Link to="/add-book">
            <Button className="mt-4 md:mt-0 bg-accent hover:bg-accent/90">
              <PlusCircle className="mr-2 h-4 w-4" />
              Adicionar Livro
            </Button>
          </Link>
        </div>

        {loading ? (
          <div className="flex justify-center items-center h-64">
            <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary"></div>
          </div>
        ) : (
          <div className="mt-12">
            <div className="flex items-center justify-between mb-6">
              <h2 className="text-2xl font-semibold text-gray-800">Livros Cadastrados</h2>
            </div>

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
              {recentBooks.map((book) => (
                <BookCard key={book.id} book={book} />
              ))}
            </div>
          </div>
        )}
      </main>
    </div>
  );
}
