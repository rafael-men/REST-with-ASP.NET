import { useState, useEffect } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import Navbar from "@/components/Navbar";
import { Button } from "@/components/ui/button";
import {
  Edit,
  Trash2,
  ArrowLeft,
  User,
  Calendar,
  Tag,
  Star,
} from "lucide-react";
import { Badge } from "@/components/ui/badge";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { useToast } from "@/components/ui/use-toast";

interface Book {
  id: number;
  title: string;
  author: string;
  price: number;
  launchDate: string;
  description: string;
  image: string;
}

export default function BookDetail() {
  const { id } = useParams<{ id: string }>();
  const [book, setBook] = useState<Book | null>(null);
  const [loading, setLoading] = useState(true);
  const { toast } = useToast();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchBook = async () => {
      setLoading(true);
      try {
        const token = localStorage.getItem("authToken");
        if (!token) {
          navigate("/login");
          return;
        }

        const response = await fetch(`http://localhost:5151/v1/api/books/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        });

        if (response.status === 401) {
          toast({
            title: "Sessão expirada",
            description: "Faça login novamente.",
            variant: "destructive",
          });
          navigate("/login");
          return;
        }

        if (!response.ok) {
          throw new Error("Livro não encontrado");
        }

        const data: Book = await response.json();
        setBook(data);
      } catch (error) {
        toast({
          title: "Erro",
          description: "Não foi possível carregar os detalhes do livro",
          variant: "destructive",
        });
        navigate("/books");
      } finally {
        setLoading(false);
      }
    };

    if (id) {
      fetchBook();
    }
  }, [id, toast, navigate]);

  const handleDelete = async () => {
    try {
      const token = localStorage.getItem("authToken");
      if (!token) {
        navigate("/login");
        return;
      }

      const response = await fetch(`http://localhost:5151/v1/api/books/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Erro ao excluir");
      }

      toast({
        title: "Sucesso",
        description: "Livro excluído com sucesso",
      });

      navigate("/main");
    } catch (error) {
      toast({
        title: "Erro",
        description: "Não foi possível excluir o livro",
        variant: "destructive",
      });
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50">
        <Navbar />
        <div className="container mx-auto px-4 py-8">
          <div className="flex justify-center items-center h-64">
            <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary"></div>
          </div>
        </div>
      </div>
    );
  }

  if (!book) return null;

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <main className="container mx-auto px-4 py-8 page-transition">
        <Link
          to="/main"
          className="inline-flex items-center text-primary mb-6 hover:underline"
        >
          <ArrowLeft className="h-4 w-4 mr-1" />
          Voltar para livros
        </Link>

        <div className="bg-white rounded-xl shadow-md overflow-hidden p-8">
          <div className="flex justify-between items-start mb-6">
            <div>
              <h1 className="text-2xl font-bold text-gray-900">{book.title}</h1>
              <div className="flex items-center mt-1 text-gray-600">
                <User className="h-4 w-4 mr-1" />
                <span>{book.author}</span>
              </div>
            </div>
            <div className="flex space-x-2">
              <Link to={`/edit-book/${book.id}`}>
                <Button variant="outline" size="icon">
                  <Edit className="h-4 w-4" />
                </Button>
              </Link>
              <AlertDialog>
                <AlertDialogTrigger asChild>
                  <Button variant="outline" size="icon" className="text-red-500">
                    <Trash2 className="h-4 w-4" />
                  </Button>
                </AlertDialogTrigger>
                <AlertDialogContent>
                  <AlertDialogHeader>
                    <AlertDialogTitle>Excluir Livro</AlertDialogTitle>
                    <AlertDialogDescription>
                      Tem certeza que deseja excluir "{book.title}"? Esta ação não pode ser desfeita.
                    </AlertDialogDescription>
                  </AlertDialogHeader>
                  <AlertDialogFooter>
                    <AlertDialogCancel>Cancelar</AlertDialogCancel>
                    <AlertDialogAction
                      onClick={handleDelete}
                      className="bg-red-500 hover:bg-red-600"
                    >
                      Excluir
                    </AlertDialogAction>
                  </AlertDialogFooter>
                </AlertDialogContent>
              </AlertDialog>
            </div>
          </div>

          {/* Imagem do livro */}
          {book.image && (
            <div className="mb-6">
              <img
                src={book.image}
                alt={`Capa do livro ${book.title}`}
                className="w-full max-w-sm mx-auto rounded-md shadow-md"
              />
            </div>
          )}

          {/* Descrição do livro */}
          {book.description && (
            <div className="mb-6 text-gray-700">
              <h2 className="text-lg font-semibold mb-1">Descrição</h2>
              <p>{book.description}</p>
            </div>
          )}

          <div className="flex flex-wrap gap-4 text-gray-700 text-sm">
            <div className="flex items-center w-full sm:w-1/2">
              <Tag className="h-4 w-4 mr-2 text-gray-500" />
              <span>
                <b>Preço:</b> R$ {book.price.toFixed(2)}
              </span>
            </div>
            <div className="flex items-center w-full sm:w-1/2">
              <Calendar className="h-4 w-4 mr-2 text-gray-500" />
              <span>
                <b>Data de lançamento:</b>{" "}
                {new Date(book.launchDate).toLocaleDateString("pt-BR")}
              </span>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
