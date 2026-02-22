import { Calendar, MapPin } from 'lucide-react';

interface SeminarCardProps {
  date: string;
  title: string;
  location: string;
  badge?: string;
}

export function SeminarCard({ date, title, location, badge }: SeminarCardProps) {
  return (
    <div className="bg-white rounded-lg p-6 shadow-lg hover:shadow-xl transition-shadow">
      {badge && (
        <div className="mb-4">
          <span className="bg-orange-500 text-white text-xs px-4 py-1 rounded-full">
            {badge}
          </span>
        </div>
      )}
      <div className="flex items-center gap-2 text-gray-700 mb-4">
        <Calendar className="w-5 h-5 text-orange-500" />
        <span className="font-semibold">{date}</span>
      </div>
      <h4 className="text-lg font-semibold text-gray-800 mb-4">{title}</h4>
      <div className="flex items-center gap-2 text-gray-600 text-sm">
        <MapPin className="w-4 h-4 text-orange-500" />
        <span>{location}</span>
      </div>
    </div>
  );
}
