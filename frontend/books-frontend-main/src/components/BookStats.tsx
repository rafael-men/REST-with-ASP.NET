
import { BookOpen, BookPlus, BookX, Book as BookIcon } from "lucide-react";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Book } from "@/types/BookTypes";

interface BookStatsProps {
  books: Book[];
}

export default function BookStats({ books }: BookStatsProps) {
  const totalBooks = books.length;
  const availableBooks = books.filter(book => book.status === "available").length;
  const borrowedBooks = books.filter(book => book.status === "borrowed").length;
  const reservedBooks = books.filter(book => book.status === "reserved").length;

  const stats = [
    {
      title: "Total de Livros",
      value: totalBooks,
      description: "Em seu acervo",
      icon: BookIcon,
      color: "text-blue-500",
      bgColor: "bg-blue-100"
    },
    {
      title: "Disponíveis",
      value: availableBooks,
      description: "Prontos para leitura",
      icon: BookOpen,
      color: "text-green-500",
      bgColor: "bg-green-100"
    },
    {
      title: "Emprestados",
      value: borrowedBooks,
      description: "Sendo lidos",
      icon: BookPlus,
      color: "text-amber-500",
      bgColor: "bg-amber-100"
    },
    {
      title: "Reservados",
      value: reservedBooks,
      description: "Na fila de espera",
      icon: BookX,
      color: "text-red-500",
      bgColor: "bg-red-100"
    }
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
      {stats.map((stat, index) => (
        <Card key={index}>
          <CardHeader className="flex flex-row items-center justify-between pb-2">
            <CardTitle className="text-sm font-medium">{stat.title}</CardTitle>
            <div className={`${stat.bgColor} p-2 rounded-full`}>
              <stat.icon className={`h-4 w-4 ${stat.color}`} />
            </div>
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{stat.value}</div>
            <p className="text-xs text-muted-foreground">{stat.description}</p>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}
